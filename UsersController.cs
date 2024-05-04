using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using REST_API_APP_for_parameter;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly FirebaseClient _firebaseClient;

        public UsersController()
        {
            _firebaseClient = new FirebaseClient(Environment.GetEnvironmentVariable("FIREBASE_PARAMETER_URL"));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.ID) || user.Genre == 0)
            {
                return BadRequest("Invalid user data.");
            }

            await _firebaseClient
                .Child("Users")
                .Child(user.ID)
                .PutAsync(user);

            return CreatedAtAction(nameof(Get), new { id = user.ID }, user);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _firebaseClient
                .Child("Users")
                .Child(id)
                .OnceSingleAsync<User>();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserParameters(string id, [FromBody] Parameter parameters)
        {
            if (parameters == null)
            {
                return BadRequest("Invalid parameters data.");
            }

            try
            {
                // Обновление только части данных пользователя
                await _firebaseClient
                    .Child("Users")
                    .Child(id)
                    .Child("Params")
                    .PatchAsync(parameters);

                return Ok("User parameters updated successfully.");
            }
            catch (Exception ex)
            {
                // Логирование ошибки или другая логика обработки исключений
                return StatusCode(500, "Error updating user parameters: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var firebaseObject = await _firebaseClient
                    .Child("Users")
                    .Child(id)
                    .OnceSingleAsync<User>();

                if (firebaseObject == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                await _firebaseClient
                    .Child("Users")
                    .Child(id)
                    .DeleteAsync();

                return Ok($"User with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting user: {ex.Message}");
            }
        }

    }
}
