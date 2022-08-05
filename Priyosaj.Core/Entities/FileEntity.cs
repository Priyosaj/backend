namespace Priyosaj.Core.Entities;

public class FileEntity : ABaseEntity
{
    public string Name { get; set; } = string.Empty;
    public FileType Type { get; set; }
    public string Url { get; set; } = string.Empty;
}

public enum FileType
{
    Image,
}