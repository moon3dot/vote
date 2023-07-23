using api.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    #region  mongodb
    private readonly IMongoCollection<Register> _collection;

    // Dependency Injection
    public RegisterController(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collection = dbName.GetCollection<Register>("users");
    }
    #endregion

    [HttpPost("register")]
    public async Task<ActionResult<Register>> create([FromBody] Register userInput)
    {
        bool hasDoc = _collection.AsQueryable().Where<Register>(u => u.Email == userInput.Email).Any();

        if (hasDoc)
            return BadRequest($"this {userInput} allrealy exist");

        Register register = new Register(
            Id: null,
            Name: userInput.Name,
            Email: userInput.Email,
            Password: userInput.Password,
            ConfirmPass: userInput.ConfirmPass,
            IsAdmin: null
        );

        await _collection.InsertOneAsync(register);

        return register;
    }
}
