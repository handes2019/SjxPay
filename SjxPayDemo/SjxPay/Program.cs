global using FastEndpoints;
global using SjxPay.Consts;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Diagnostics;
using SjxPay.Common;
using SjxPay.Common.Accessor;
using SjxPay.Core;
using System.Net;
using SjxPay.Wechatpay;
using SjxPay.Domain;
using SjxPay.AliPay;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(builder =>
{
    builder.AddLog4Net("log4net.config");
});
//设置Kestrel最大请求限制
builder.WebHost.ConfigureKestrel(x =>
{
    x.Limits.MaxRequestBodySize = 200 * 1024 * 1024;
});
builder.Services.AddHttpClient();
builder.Services.AddFastEndpoints();
builder.Services.AddAuthentication()
    .AddOAuth2Introspection("Bearer", options =>
    {
        //options.Authority = "https://passport-dev.shixianjia.com";
        //options.IntrospectionEndpoint = "http://passport_dev/connect/introspect";

        options.Authority = "https://passport.shixianjia.com";
        options.ClientId = "house";
        options.ClientSecret = "DJiefiIE2835946";
    });
builder.Services.AddSwaggerDoc(addJWTBearerAuth: true);
builder.Services.AddScoped<IClaimsAccessor, ClaimsAccessor>();
builder.Services.AddScoped<IPrincipalAccessor, PrincipalAccessor>();
builder.Services.AddSjxPay(a =>
{
    IConfiguration configuration = builder.Services.GetConfiguration();
    a.UseWechatpay(configuration);
    a.UseAlipay(configuration);
});
await builder.Services.AddApplication<DomainModule>().InitializeAsync(builder.Services.BuildServiceProviderFromFactory());
builder.Services.BuildServiceProvider();
var app = builder.Build();
//设置跨域
//app.UseCors(x => x
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .SetIsOriginAllowed(origin => true) // allow any origin
//    .AllowCredentials());

app.UseRouting();
app.UseAuthentication();//启用jw认证
app.UseFastEndpoints(s =>
{
    s.Serializer.Options.PropertyNamingPolicy = null;
});
app.UseSwaggerGen();
//全局异常处理
app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        context.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
        var exception = context.Features.Get<IExceptionHandlerFeature>();
        if (exception != null)
        {
            var error = CustomResponse.ServerError(exception.Error.Message);
            await context.Response.WriteAsJsonAsync(error).ConfigureAwait(false);
        }
    });
});


app.UseSjxPay();


app.Run();
