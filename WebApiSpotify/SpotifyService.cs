using SpotifyAPI.Web;

public class SpotifyService
{
    private readonly SpotifyClient _spotifyClient;
    private readonly ClientCredentialsAuthenticator _authenticator;

    public SpotifyService(string clientId, string clientSecret)
    {
        _authenticator = new ClientCredentialsAuthenticator(clientId, clientSecret);
        var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(_authenticator);
        _spotifyClient = new SpotifyClient(config);
    }

    public async Task<AvailableMarketsResponse?> GetAvailableMarkets()
    {
        try
        {
            return await _spotifyClient.Markets.AvailableMarkets();
        }
        catch (APIException ex)
        {
            Console.WriteLine($"Error retrieving available Markets: {ex.Message}");
            return null;
        }
    }

    public async Task<ArtistsResponse?> GetArtists(IEnumerable<string> ids)
    {
        try
        {
            var request = new ArtistsRequest((IList<string>)ids);

            return await _spotifyClient.Artists.GetSeveral(request);
        }
        catch (APIException ex)
        {
            Console.WriteLine($"Error retrieving Artists: {ex.Message}");
            return null;
        }
    }

    public async Task<ArtistsTopTracksResponse?> GetArtistTopTracks(string artistId, string market)
    {
        try
        {
            var request = new ArtistsTopTracksRequest(market);

            return await _spotifyClient.Artists.GetTopTracks(artistId, request);
        }
        catch (APIException ex)
        {
            Console.WriteLine($"Error retrieving Artist's Top Tracks: {ex.Message}");
            return null;
        }
    }

    public async Task<CategoriesResponse?> GetCategories(string market = "US", int limit = 10, int offset = 0)
    {
        try
        {
            var request = new CategoriesRequest
            {
                Country = market,
                Limit = limit,
                Offset = offset,
            };
            return await _spotifyClient.Browse.GetCategories(request);
        }
        catch (APIException ex)
        {
            Console.WriteLine($"Error retrieving Categories: {ex.Message}");
            return null;
        }
    }

    public async Task<NewReleasesResponse?> GetNewReleases(int limit = 10, int offset = 0)
    {
        try
        {
            var request = new NewReleasesRequest
            {
                Limit = limit,
                Offset = offset,
            };
            return await _spotifyClient.Browse.GetNewReleases(request);
        }
        catch (APIException ex)
        {
            Console.WriteLine($"Error retrieving New Releases: {ex.Message}");
            return null;
        }
    }

    public async Task<FullTrack?> GetTrack(string trackId = "11dFghVXANMlKmJXsNCbNl")
    {
        try
        {
            return await _spotifyClient.Tracks.Get(trackId);
        }
        catch (APIException ex)
        {
            Console.WriteLine($"Error retrieving Track: {ex.Message}");
            return null;
        }
    }

    public async Task<TracksResponse?> GetTracks(IEnumerable<string> ids, string market = "US")
    {
        try
        {
            var request = new TracksRequest((IList<string>)ids)
            {
                Market = market
            };
            return await _spotifyClient.Tracks.GetSeveral(request);
        }
        catch (APIException ex)
        {
            Console.WriteLine($"Error retrieving Tracks: {ex.Message}");
            return null;
        }
    }

    public async Task<SearchResponse?> GetSearch(string query, SearchRequest.Types type = SearchRequest.Types.Album, string market = "US", int limit = 5, int offset = 0)
    {
        try
        {
            var request = new SearchRequest(type, query) { Market = market, Limit = limit, Offset = offset };
            return await _spotifyClient.Search.Item(request);
        }
        catch (APIException ex)
        {
            Console.WriteLine($"Error retrieving search: {ex.Message}");
            return null;
        }
    }
}
