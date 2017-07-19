<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

public const string _meetupDotComEventId = "241048316";

async Task Main()
{
    // Get list of attendees
    
    var attendees = await GetAttendees();
    
    // Add completely pointless delay to add a bit of suspense!
    
    await RunPointlessProgressBar();
    
    // Randomize a winner!

    var winner = PickRandomAttendee(attendees);

    // Write winner's details to a file

    File.AppendAllText(
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LINQPadPrizeDrawWinner.txt"),
        JsonConvert.SerializeObject(winner, Newtonsoft.Json.Formatting.Indented));

    // Dump the winner to the output window

    new
    {
        winner.Name,
        ProfileUri = new Hyperlinq ($"https://www.meetup.com/dotnetoxford/members/{winner.AttendeeId}/", "Profile Page"),
        Image = Util.Image(winner.ImageUri) ?? Util.Image("https://tinyurl.com/ybh4jwmm"),
    }.Dump();
}

private async Task<AttendeeModel[]> GetAttendees()
{
    using (var client = new HttpClient())
    {
        var eventId = _meetupDotComEventId;

        client.BaseAddress = new Uri("https://api.meetup.com");

        var response = await client.GetAsync($"/dotnetoxford/events/{eventId}/rsvps");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var rsvps = JsonConvert.DeserializeObject<List<MeetupComResponseModel>>(content);
        
        return rsvps.Where(x => x.response == "yes").Select(x => new AttendeeModel
        {
            Name = x.member.name,
            AttendeeId = x.member.id,
            ImageUri = x.member.photo?.highres_link ?? x.member.photo?.photo_link,
        }).ToArray();
    }
}

private async Task RunPointlessProgressBar()
{
    var progressBar = new Util.ProgressBar(true);
    progressBar.Dump();

    for (int i = 1; i <= 100; i++)
    {
        await Task.Delay(50);
        progressBar.Percent = i;
    }
}

private AttendeeModel PickRandomAttendee(AttendeeModel[] attendees)
{
    var rand = new Random();
    return attendees[rand.Next(0, attendees.Count())];
}

public class AttendeeModel
{
    public string Name { get; set; }
    public int AttendeeId { get; set; }
    public string ImageUri { get; set; }

    public string Image => ImageUri ?? @"Images\NoPhoto.png";
}

public class MeetupComResponseModel
{
    public long created { get; set; }
    public long updated { get; set; }
    public string response { get; set; }
    public int guests { get; set; }
    public Event _event { get; set; }
    public Group group { get; set; }
    public Member member { get; set; }
    public Venue venue { get; set; }
}

public class Event
{
    public string id { get; set; }
    public string name { get; set; }
    public int yes_rsvp_count { get; set; }
    public long time { get; set; }
    public int utc_offset { get; set; }
}

public class Group
{
    public int id { get; set; }
    public string urlname { get; set; }
    public string name { get; set; }
    public string who { get; set; }
    public int members { get; set; }
    public string join_mode { get; set; }
    public Group_Photo group_photo { get; set; }
}

public class Group_Photo
{
    public int id { get; set; }
    public string highres_link { get; set; }
    public string photo_link { get; set; }
    public string thumb_link { get; set; }
    public string type { get; set; }
    public string base_url { get; set; }
}

public class Member
{
    public int id { get; set; }
    public string name { get; set; }
    public Photo photo { get; set; }
    public Event_Context event_context { get; set; }
    public string role { get; set; }
    public string bio { get; set; }
}

public class Photo
{
    public int id { get; set; }
    public string highres_link { get; set; }
    public string photo_link { get; set; }
    public string thumb_link { get; set; }
    public string type { get; set; }
    public string base_url { get; set; }
}

public class Event_Context
{
    public bool host { get; set; }
}

public class Venue
{
    public int id { get; set; }
    public string name { get; set; }
    public float lat { get; set; }
    public float lon { get; set; }
    public bool repinned { get; set; }
    public string address_1 { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string localized_country_name { get; set; }
}