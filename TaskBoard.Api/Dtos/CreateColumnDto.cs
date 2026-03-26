namespace TaskBoard.Api.Dtos
{
    public class CreateColumnDto
    {
        public string Name { get; set; } = default!;
        public int Order { get; set; }
    }
}
