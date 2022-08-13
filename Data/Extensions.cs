namespace DotnetTest.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<PizzaContext>();
                // DO NOT USE IN PRODUCTION!
                context.Database.EnsureCreated();
                DbInitializer.initialize(context);
            }
        }
    }
}