namespace WebApiSpotify
{
    public static class Extensions
    {
        public static void AddSpotifyService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SpotifyService>(provider =>
            {
                var clientId = configuration["Spotify:ClientId"];
                var clientSecret = configuration["Spotify:ClientSecret"];
                return new SpotifyService(clientId, clientSecret);
            });
        }
    }
}
