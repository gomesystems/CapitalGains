using CapitalGains.Domain.Services.Interface;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona os servi�os de controle e de documenta��o Swagger.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Inje��o de depend�ncia
        builder.Services.AddScoped<ITradeOperationValidator, TradeOperationValidator>();

        var app = builder.Build();

        // Configura o middleware.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
