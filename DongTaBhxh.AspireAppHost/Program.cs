var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.DongTaBhxh_ApiGateway>("DongTaBhxh-ApiGateway");
builder.AddProject<Projects.DongTaBhxh_WebUI>("DongTaBhxh-WebUI");
builder.Build().Run();