using System.Net;
using MyFirstWebSite;

#region ExtraCode
//var listener = new HttpListener();
//listener.Prefixes.Add("http://127.0.0.1:27001/");
//listener.Start();

//while (true)
//{
//    HttpListenerContext? context = await listener.GetContextAsync();

//    HttpListenerRequest? request = context.Request;
//    HttpListenerResponse? response = context.Response;


//}
#endregion

WebHost myHost = new WebHost(port: 27001);
myHost.Run();