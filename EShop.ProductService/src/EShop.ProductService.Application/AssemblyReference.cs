using System.Reflection;

namespace EShop.ProductService.Application;

public class AssemblyReference
{
    public static readonly Assembly Executing = Assembly.GetExecutingAssembly();
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
