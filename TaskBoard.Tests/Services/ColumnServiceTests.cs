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
    public class ColumnServiceTests
    {
        private AppDbContext _db = null!;
        private ColumnService _service = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _db = new AppDbContext(options);
            var repo = new ColumnRepository(_db);
            _service = new ColumnService(repo, _db);
        }

        [TearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public async Task CreateColumn_ShouldPersist()
        {
            var col = new Column { Name = "QA", Order = 3 };
            var created = await _service.CreateColumnAsync(col);
            var saved = await _db.Columns.FindAsync(created.Id);

            Assert.IsNotNull(saved);
            Assert.AreEqual("QA", saved!.Name);
            Assert.AreEqual(3, saved.Order);
        }

        [Test]
        public async Task ListColumns_ShouldReturnAll()
        {
            await _service.CreateColumnAsync(new Column { Name = "A", Order = 0 });
            await _service.CreateColumnAsync(new Column { Name = "B", Order = 1 });

            var list = (await _service.ListColumnsAsync()).ToList();

            Assert.GreaterOrEqual(list.Count, 2);
            Assert.IsTrue(list.Any(c => c.Name == "A"));
            Assert.IsTrue(list.Any(c => c.Name == "B"));
        }

        [Test]
        public async Task GetColumn_WithTasks_ReturnsColumnAndTasks()
        {
            var col = new Column { Name = "WithTasks", Order = 10 };
            var created = await _service.CreateColumnAsync(col);

            // add tasks directly to DB to simulate existing tasks
            _db.Tasks.Add(new TaskItem { Name = "T1", ColumnId = created.Id });
            _db.Tasks.Add(new TaskItem { Name = "T2", ColumnId = created.Id, IsFavorited = true });
            await _db.SaveChangesAsync();

            var fetched = await _service.GetColumnAsync(created.Id);

            Assert.IsNotNull(fetched);
            Assert.AreEqual(created.Id, fetched!.Id);
            Assert.IsNotNull(fetched.Tasks);
            Assert.AreEqual(2, fetched.Tasks.Count);
        }
    }
}
