using AutoAid.Application.Firebase;
using AutoAid.Domain.Common;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace AutoAid.Infrastructure.Firebase;

public class FirebaseClient : IFirebaseClient
{
    private FirebaseAuth? _auth;
    private FirebaseApp? _app;

    public FirebaseClient()
    {
    }

    private FirebaseApp App => _app ??= FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(AppConfig.FirebaseConfig.DefaultPath)
    });


    public FirebaseAuth FirebaseAuth
    {
        get
        {
            return _auth ??= FirebaseAuth.GetAuth(App);
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
            _app?.Delete();
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

