using System;
using System.Net;
using System.Text;

namespace MyFirstWebSite;

public class WebHost
{
	private short _port;
	private HttpListener _listener;

	public WebHost(short port)
	{
		_port = port;
	}

	public async void Run()
	{
		_listener = new HttpListener();
		_listener.Prefixes.Add($"http://localhost:{_port}/");
		_listener.Start();
		Console.WriteLine($"Http Server Started on {_port}");

		while (true)
		{
            var context =  _listener.GetContext();
            _ = Task.Run(() =>
            {
                HandlerRequest(context);
            });
        }
	}

	private void HandlerRequest(HttpListenerContext? context)
	{
        var request = context?.Request;
        var response = context?.Response;
        
        //var writer = new StreamWriter(response.OutputStream); bunun yerine basqa sey istifade edekki hemde sekilleri oxusun

        string url = request?.RawUrl;
		var path = url?.Substring(1);
		Console.WriteLine(path);
        
        if (!Path.HasExtension(path)) path += ".html";
        response.ContentType = GetContentType(path);
        try
		{
            if (File.Exists($"/Users/fermayilhesenov/Projects/MyFirstWebSite/MyFirstWebSite/Views/{path}"))
            {
                response.StatusCode = 200;
                var bytes = File.ReadAllBytes($"/Users/fermayilhesenov/Projects/MyFirstWebSite/MyFirstWebSite/Views/{path}");
                //writer.Write(text);
                response.OutputStream.Write(bytes);
            }
            else
            {
                response.StatusCode = 404;
                var bytes = File.ReadAllBytes($"/Users/fermayilhesenov/Projects/MyFirstWebSite/MyFirstWebSite/Views/404.html");
                //writer.Write(text);
                response.OutputStream.Write(bytes);
            }
        }
		catch(Exception exc)
		{
			response.StatusCode = 500;
            //writer.Write(exc.ToString());
            response.OutputStream.Write(Encoding.UTF8.GetBytes(exc.ToString()));

		}
		finally
		{
            response?.Close();
            //writer.Dispose();
        }
    }


    public string GetContentType(string path)
    {
        string contentType;

        string extension = Path.GetExtension(path).ToLower();

        switch (extension)
        {
            case ".css":
                contentType = "text/css";
                break;
            case ".js":
                contentType = "text/javascript";
                break;
            case ".html":
            case ".htm":
                contentType = "text/html";
                break;
            case ".png":
                contentType = "image/png";
                break;
            case ".jpg":
            case ".jpeg":
                contentType = "image/jpeg";
                break;
            case ".gif":
                contentType = "image/gif";
                break;
            case ".ico":
                contentType = "image/x-icon";
                break;
            case ".json":
                contentType = "application/json";
                break;
            case ".xml":
                contentType = "application/xml";
                break;
            case ".pdf":
                contentType = "application/pdf";
                break;
            case ".zip":
                contentType = "application/zip";
                break;
            case ".mp4":
                contentType = "video/mp4";
                break;
            case ".mp3":
                contentType = "audio/mpeg";
                break;
            default:
                contentType = "application/octet-stream"; // Bilinmeyen türler için varsayılan değer
                break;
        }

        return contentType;
    }

}

