using SBaier.DI;

namespace SBaier.AI.Samples
{
    public class AgentsInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.Bind<ReadonlyObservable<string>>()
                .And<Observable<string>>()
                .ToNew<Observable<string>>()
                .AsSingle();
        }
    }
}