using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;

[ApiController]
[Route("[controller]")]
public class SpotifyController : ControllerBase
{
    private readonly SpotifyService _spotifyService;

    public SpotifyController(SpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    /// <summary>
    /// A markets object with an array of country codes
    /// </summary>
    /// <returns>A markets object with an array of country codes.
    /// Example: ["CA","BR","IT"]</returns>
    [HttpGet("/markets")]
    public async Task<ActionResult<AvailableMarketsResponse>> GetAvailableMarkets()
    {
        var markets = await _spotifyService.GetAvailableMarkets();
        if (markets == null)
        {
            return NotFound();
        }
        return Ok(markets);
    }

    /// <summary>
    /// Get Spotify catalog information for several artists based on their Spotify IDs.
    /// </summary>
    /// <remarks>
    /// This endpoint returns information about artists based on their IDs.
    /// </remarks>
    /// <param name="ids">Required
    /// A comma-separated list of the Spotify IDs for the artists.Maximum: 100 IDs.</param>
    /// <returns>A set of artists</returns>
    /// <response code="200">Returns the requested artists.</response>
    /// <response code="404">If no artists are found.</response>
    [HttpGet("/artists")]
    public async Task<ActionResult<ArtistsResponse>> GetArtists([FromQuery] IEnumerable<string> ids) // 2CIMQHirSU0MQqyYHq0eOx, 57dN52uHvrHOxijzpIgu3E, 1vCWHaC5f2uS3yhpwWbIA6
    {
        var artists = await _spotifyService.GetArtists(ids);
        if (artists == null)
        {
            return NotFound();
        }
        return Ok(artists);
    }

    /// <summary>
    /// Get Spotify catalog information about an artist's top tracks by country.
    /// </summary>
    /// <param name="artistId">Required
    /// The Spotify ID of the artist.
    /// Example: 0TnOYISbd1XYRBk9myaseg</param>
    /// <param name="market">Example: market=ES</param>
    /// <returns>A set of tracks</returns>
    [HttpGet("/artists/{id}/top-tracks")]
    public async Task<ActionResult<ArtistsTopTracksResponse>> GetArtistTopTracks(string artistId = "0TnOYISbd1XYRBk9myaseg", string market = "US")
    {
        var tops = await _spotifyService.GetArtistTopTracks(artistId, market);
        if (tops == null)
        {
            return NotFound();
        }
        return Ok(tops);
    }

    /// <summary>
    /// Get a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
    /// </summary>
    /// <param name="locale">Example: locale=sv_SE</param>
    /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
    /// <param name="offset">The index of the first item to return. Default: 0 (the first item). Use with limit to get the next set of items.</param>
    /// <returns>A paged set of categories</returns>
    [HttpGet("/browse/categories")]
    public async Task<ActionResult<CategoriesResponse>> GetCategories(string locale = "US", int limit = 10, int offset = 0)
    {
        var categories = await _spotifyService.GetCategories(locale, limit, offset);
        if (categories == null)
        {
            return NotFound();
        }
        return Ok(categories);
    }

    /// <summary>
    /// Get New Releases
    /// </summary>
    /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
    /// <returns>A set of new releases</returns>
    [HttpGet("/browse/newreleases")]
    public async Task<ActionResult<NewReleasesResponse>> GetNewReleases(int limit = 10)
    {
        var newReleases = await _spotifyService.GetNewReleases(limit);
        if (newReleases == null)
        {
            return NotFound();
        }
        return Ok(newReleases);
    }

    /// <summary>
    /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
    /// </summary>
    /// <param name="id">Required 
    /// The Spotify ID for the track.
    /// Example: 11dFghVXANMlKmJXsNCbNl</param>
    /// <returns>A track</returns>
    [HttpGet("/track/{id}")]
    public async Task<ActionResult<FullTrack>> GetTrack(string id = "11dFghVXANMlKmJXsNCbNl")
    {
        var track = await _spotifyService.GetTrack(id);
        if (track == null)
        {
            return NotFound();
        }
        return Ok(track);
    }

    /// <summary>
    /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
    /// </summary>
    /// <param name="ids">Required
    /// A comma-separated list of the Spotify IDs.For example: ids= 4iV5W9uYEdYUVa79Axb7Rh,1301WleyT98MSxVHPZCA6M.Maximum: 100 IDs.</param>
    /// <param name="market">Users can view the country that is associated with their account in the account settings.
    /// Example: market=ES</param>
    /// <returns>A set of tracks</returns>
    [HttpGet("/tracks")]
    public async Task<ActionResult<TracksResponse>> GetTracks([FromQuery] IEnumerable<string> ids, string market = "US") // 7ouMYWpwJ422jRcDASZB7P, 4VqPOruhp5EdPBeR92t6lQ
    {
        var tracks = await _spotifyService.GetTracks(ids, market);
        if (tracks == null)
        {
            return NotFound();
        }
        return Ok(tracks);
    }

    /// <summary>
    /// Get Spotify catalog information about albums, artists, playlists, tracks, shows, episodes or audiobooks that match a keyword string.
    /// Audiobooks are only available within the US, UK, Canada, Ireland, New Zealand and Australia markets.
    /// </summary>
    /// <param name="query">Required
    /// Your search query.
    /// You can narrow down your search using field filters. The available filters are album, artist, track, year, upc, tag:hipster, tag:new, isrc, and genre. Each field filter only applies to certain result types.
    /// The artist and year filters can be used while searching albums, artists and tracks. You can filter on a single year or a range (e.g. 1955-1960).
    /// The album filter can be used while searching albums and tracks.
    /// The genre filter can be used while searching artists and tracks.
    /// The isrc and track filters can be used while searching tracks.
    /// The upc, tag:new and tag:hipster filters can only be used while searching albums. The tag:new filter will return albums released in the past two weeks and tag:hipster can be used to return only albums with the lowest 10% popularity.
    /// Example: q = remaster % 2520track % 3ADoxy % 2520artist % 3AMiles % 2520Davis </param>
    /// <param name="type">Required
    /// A comma-separated list of item types to search across.Search results include hits from all the specified item types.For example: q= abacab & type = album, track returns both albums and tracks matching "abacab".
    /// Allowed values: "album", "artist", "playlist", "track", "show", "episode", "audiobook"</param>
    /// <param name="market">Example: market=ES</param>
    /// <param name="limit">The maximum number of results to return in each item type.</param>
    /// <param name="offset">The index of the first result to return. Use with limit to get the next page of search results.
    /// Default: offset=0 Range: 0 - 1000 Example: offset=5</param>
    /// <returns>Search response</returns>
    [HttpGet("/search")]
    public async Task<ActionResult<SearchResponse>> GetSearch(string query = "remaster%20track:Doxy%20artist:Miles%20Davis",
        SearchRequest.Types type = SearchRequest.Types.Album, string market = "US", int limit = 5, int offset = 0)
    {
        var s = await _spotifyService.GetSearch(query, type, market, limit, offset);
        if (s == null)
        {
            return NotFound();
        }
        return Ok(s);
    }

}
