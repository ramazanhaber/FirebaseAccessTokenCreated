using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Windows.Forms;


namespace FirebaseAccessTokenCreated
{
    public partial class Form1 : Form
    {

        /*
         1- Firebase console
         2- Project setttings
         3- Service Accounds
         4- Generate new private key
         5- rename json file adminsdk.json
         6- debug move file -> adminsdk.json
        */
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeFirebase();
        }
        private void InitializeFirebase()
        {
            try
            {
                string jsonPath = "adminsdk.json"; // JSON dosyanızın yolu
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);

                var credential = GoogleCredential.FromFile(jsonPath).CreateScoped(new[]
                {
                "https://www.googleapis.com/auth/firebase.messaging"
            });

                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = credential,
                    });
                }

                var token = credential.UnderlyingCredential.GetAccessTokenForRequestAsync().Result;

                Clipboard.SetText(token);

                txtAccessToken.Text = token;
                MessageBox.Show("Panoya Kopyalandi -> Access Token: " + token);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing Firebase: " + ex.Message);
            }
        }


    }
}
