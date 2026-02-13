namespace OrdersWeb.SecurityDemos;

public sealed class FilesDemoService
{
    // VULNERABLE: tainted input becomes part of a filesystem path
    public string ReadFileVulnerable(string name, string contentRootPath)
    {
        var baseDir = Path.Combine(contentRootPath, "DemoFiles");
        var target = Path.Combine(baseDir, name);      // <-- tainted input into path
        return File.ReadAllText(target);               // <-- sink
    }

    // SAFE (training fix): allow only .txt files and strip directories
    public string ReadFileSafe(string name, string contentRootPath)
    {
        var safeName = Path.GetFileName(name);
        if (!safeName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Only .txt allowed");

        var baseDir = Path.Combine(contentRootPath, "DemoFiles");
        var target = Path.Combine(baseDir, safeName);
        return File.ReadAllText(target);
    }
}
