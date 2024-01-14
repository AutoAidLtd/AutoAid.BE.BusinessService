using AutoAid.Application.Firebase;
using AutoAid.Domain.Common;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace AutoAid.Infrastructure.Firebase;

public class FirebaseClient : IFirebaseClient
{
    private FirebaseAuth? _auth;
    private readonly FirebaseApp _app;

    public FirebaseClient()
    {
         _app = FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(AppConfig.FirebaseConfig.Path)
        });
    }

    public FirebaseAuth? FirebaseAuth
    {
        get
        {
            try
            {
                return _auth ??= FirebaseAuth.GetAuth(_app);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }

    #region Destructor
    private bool _isDisposed = false;

    public void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _app.Delete();
        }

        _isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~FirebaseClient()
    {
        Dispose(false);
    }
    #endregion Destructor



}

