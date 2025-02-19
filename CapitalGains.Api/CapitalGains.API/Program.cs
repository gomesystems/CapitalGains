using CapitalGains.Domain.Services.Interface;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona os serviços de controle e de documentação Swagger.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Injeção de dependência
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
