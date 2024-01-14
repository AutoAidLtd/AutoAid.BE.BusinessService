using FirebaseAdmin.Auth;

namespace AutoAid.Application.Firebase
{
    public interface IFirebaseClient : IDisposable
    {
        public FirebaseAuth? FirebaseAuth { get; }
    }
}
