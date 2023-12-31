using api.Models;
using api.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : ControllerBase
{
    #region  MONGOdDB
    private readonly IMongoCollection<Vote> _collection;
    // Dependency Injection
    public VoteController(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Vote>("vote");
    }
    #endregion

    #region  post vote

    [HttpPost("Vote")]
    public async Task<ActionResult<Vote>> Vote(Vote userInput)
    {

        Vote vote = new Vote(
            Id: null,
            Saturday: userInput.Saturday | false,
            SaturdayTime: new Time
            (
                Prd10To14: userInput.SaturdayTime?.Prd10To14 | false,
                Prd11To15: userInput.SaturdayTime?.Prd11To15 | false,
                Prd12To16: userInput.SaturdayTime?.Prd12To16 | false,
                Prd13To17: userInput.SaturdayTime?.Prd13To17 | false,
                Prd14To18: userInput.SaturdayTime?.Prd14To18 | false,
                Prd15To19: userInput.SaturdayTime?.Prd15To19 | false,
                Prd16To20: userInput.SaturdayTime?.Prd16To20 | false,
                Prd17To21: userInput.SaturdayTime?.Prd17To21 | false
            ),
            Sunday: userInput.Sunday | false,
            SundayTime: new Time
            (
                Prd10To14: userInput.SundayTime?.Prd10To14 | false,
                Prd11To15: userInput.SundayTime?.Prd11To15 | false,
                Prd12To16: userInput.SundayTime?.Prd12To16 | false,
                Prd13To17: userInput.SundayTime?.Prd13To17 | false,
                Prd14To18: userInput.SundayTime?.Prd14To18 | false,
                Prd15To19: userInput.SundayTime?.Prd15To19 | false,
                Prd16To20: userInput.SundayTime?.Prd16To20 | false,
                Prd17To21: userInput.SundayTime?.Prd17To21 | false
            ),
            Monday: userInput.Monday | false,
            MondayTime: new Time
            (
                Prd10To14: userInput.MondayTime?.Prd10To14 | false,
                Prd11To15: userInput.MondayTime?.Prd11To15 | false,
                Prd12To16: userInput.MondayTime?.Prd12To16 | false,
                Prd13To17: userInput.MondayTime?.Prd13To17 | false,
                Prd14To18: userInput.MondayTime?.Prd14To18 | false,
                Prd15To19: userInput.MondayTime?.Prd15To19 | false,
                Prd16To20: userInput.MondayTime?.Prd16To20 | false,
                Prd17To21: userInput.MondayTime?.Prd17To21 | false
            ),
            Tuesday: userInput.Tuesday | false,
            TuesdayTime: new Time
            (
                Prd10To14: userInput.TuesdayTime?.Prd10To14 | false,
                Prd11To15: userInput.TuesdayTime?.Prd11To15 | false,
                Prd12To16: userInput.TuesdayTime?.Prd12To16 | false,
                Prd13To17: userInput.TuesdayTime?.Prd13To17 | false,
                Prd14To18: userInput.TuesdayTime?.Prd14To18 | false,
                Prd15To19: userInput.TuesdayTime?.Prd15To19 | false,
                Prd16To20: userInput.TuesdayTime?.Prd16To20 | false,
                Prd17To21: userInput.TuesdayTime?.Prd17To21 | false
            ),
            Wednesday: userInput.Wednesday | false,
            WednesdayTime: new Time
            (
                Prd10To14: userInput.WednesdayTime?.Prd10To14 | false,
                Prd11To15: userInput.WednesdayTime?.Prd11To15 | false,
                Prd12To16: userInput.WednesdayTime?.Prd12To16 | false,
                Prd13To17: userInput.WednesdayTime?.Prd13To17 | false,
                Prd14To18: userInput.WednesdayTime?.Prd14To18 | false,
                Prd15To19: userInput.WednesdayTime?.Prd15To19 | false,
                Prd16To20: userInput.WednesdayTime?.Prd16To20 | false,
                Prd17To21: userInput.WednesdayTime?.Prd17To21 | false
            ),
            Thursday: userInput.Thursday | false,
            ThursdayTime: new Time
            (
                Prd10To14: userInput.ThursdayTime?.Prd10To14 | false,
                Prd11To15: userInput.ThursdayTime?.Prd11To15 | false,
                Prd12To16: userInput.ThursdayTime?.Prd12To16 | false,
                Prd13To17: userInput.ThursdayTime?.Prd13To17 | false,
                Prd14To18: userInput.ThursdayTime?.Prd14To18 | false,
                Prd15To19: userInput.ThursdayTime?.Prd15To19 | false,
                Prd16To20: userInput.ThursdayTime?.Prd16To20 | false,
                Prd17To21: userInput.ThursdayTime?.Prd17To21 | false
            ),
            Friday: userInput.Friday | false,
            FridayTime: new Time
            (
                Prd10To14: userInput.FridayTime?.Prd10To14 | false,
                Prd11To15: userInput.FridayTime?.Prd11To15 | false,
                Prd12To16: userInput.FridayTime?.Prd12To16 | false,
                Prd13To17: userInput.FridayTime?.Prd13To17 | false,
                Prd14To18: userInput.FridayTime?.Prd14To18 | false,
                Prd15To19: userInput.FridayTime?.Prd15To19 | false,
                Prd16To20: userInput.FridayTime?.Prd16To20 | false,
                Prd17To21: userInput.FridayTime?.Prd17To21 | false
            )
        );

        await _collection.InsertOneAsync(vote);

        return vote;

    }

    #endregion

    #region  get all
    [HttpGet("get-all")]
    public ActionResult<IEnumerable<Vote>> GetAll()
    {
        List<Vote> votes = _collection.Find<Vote>(new BsonDocument()).ToList();

        if (!votes.Any())
            return NoContent();

        return votes;
    }

    #endregion
}
