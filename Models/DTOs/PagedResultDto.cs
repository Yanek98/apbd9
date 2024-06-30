namespace APBD09.Models.DTOs;

public class PagedResultDto<T>
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<T> Items { get; set; }
}