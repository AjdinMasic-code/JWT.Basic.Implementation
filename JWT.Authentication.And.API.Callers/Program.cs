// See https://aka.ms/new-console-template for more information
using JWT.Authentication.And.API.Callers.RequestHandlers;

var authenticateHandler = new AuthenticateHandler();

Console.WriteLine("starting failure path.....");
//Testing that the authorize attribute actually works....
await authenticateHandler.SendFailureRequest();

Console.WriteLine("starting success path.....");
// proper handling

await authenticateHandler.SendRequestAsync();

Console.ReadKey();