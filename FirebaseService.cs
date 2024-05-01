using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace REST_API_APP_for_parameter
{
    internal class FirebaseService
    {
        private const string path = "../../FB_ADMINSDK_KEY.json";

        public static FirebaseApp? InitializeFirebaseApp()
        {
            try
            {
                var app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                });
                Console.WriteLine("Firebase App initialized successfully.");
                return app; // Инициализированный экземпляр FirebaseApp
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to initialize Firebase App: " + ex.Message);
                return null; // В случае ошибки возвращаем null
            }
        }
    }
}