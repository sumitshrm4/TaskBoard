using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TaskBoard.Api.Data;
using TaskBoard.Api.Models;
using TaskBoard.Api.Repositories;
using TaskBoard.Api.Services;

namespace TaskBoard.Tests.Services
{
    [TestFixture]
    public class TaskServiceTests
    {
        private AppDbContext _db = null!;
        private TaskService _service = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _db = new AppDbContext(options);

            // Ensure seed columns exist for tests that rely on columns
            if (!_db.Columns.Any())
            {
                _db.Columns.AddRange(
                    new Column { Id = Guid.NewGuid(), Name = "ToDo", Order = 0 },
                    new Column { Id = Guid.NewGuid(), Name = "In Progress", Order = 1 },
                    new Column { Id = Guid.NewGuid(), Name = "Done", Order = 2 }
                );
                _db.SaveChanges();
            }

            var repo = new TaskRepository(_db);
            _service = new TaskService(repo, _db);
        }

        [TearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public async Task CreateTask_ShouldPersistTask()
        {
            var columnId = _db.Columns.First().Id;
            var dto = new TaskItem { Name = "Test Task", ColumnId = columnId };

            var created = await _service.CreateTaskAsync(dto);
            var saved = await _db.Tasks.FindAsync(created.Id);

            Assert.IsNotNull(saved);
            Assert.AreEqual("Test Task", saved!.Name);
            Assert.AreEqual(columnId, saved.ColumnId);
        }

        [Test]
        public async Task UpdateTask_ShouldModifyFields()
        {
            var columnId = _db.Columns.First().Id;
            var dto = new TaskItem { Name = "Initial", ColumnId = columnId };
            var created = await _service.CreateTaskAsync(dto);

            var update = new TaskItem
            {
                Name = "Updated",
                Description = "desc",
                IsFavorited = true,
                Deadline = DateTime.UtcNow.AddDays(3),
                SortOrder = 5
            };

            var updated = await _service.UpdateTaskAsync(created.Id, update);

            Assert.IsNotNull(updated);
            Assert.AreEqual("Updated", updated!.Name);
            Assert.AreEqual("desc", updated.Description);
            Assert.IsTrue(updated.IsFavorited);
            Assert.AreEqual(5, updated.SortOrder);
        }

        [Test]
        public async Task DeleteTask_ShouldRemove()
        {
            var columnId = _db.Columns.First().Id;
            var dto = new TaskItem { Name = "ToDelete", ColumnId = columnId };
            var created = await _service.CreateTaskAsync(dto);

            var deleted = await _service.DeleteTaskAsync(created.Id);

            Assert.IsTrue(deleted);
            var found = await _db.Tasks.FindAsync(created.Id);
            Assert.IsNull(found);
        }

        [Test]
        public async Task MoveTask_ShouldChangeColumnId()
        {
            var columns = _db.Columns.Take(2).ToArray();
            var source = columns[0].Id;
            var target = columns[1].Id;

            var dto = new TaskItem { Name = "Movable", ColumnId = source };
            var created = await _service.CreateTaskAsync(dto);

            var moved = await _service.MoveTaskAsync(created.Id, target);

            Assert.IsTrue(moved);
            var saved = await _db.Tasks.FindAsync(created.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(target, saved!.ColumnId);
        }

        [Test]
        public async Task GetTask_NonExistent_ReturnsNull()
        {
            var result = await _service.GetTaskAsync(Guid.NewGuid());
            Assert.IsNull(result);
        }
    }
}
