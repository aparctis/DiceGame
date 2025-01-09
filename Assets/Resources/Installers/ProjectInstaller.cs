using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SaveLoadSystem saveLoadSystemPrefab;
    [SerializeField] private LevelLoader levelLoaderPrefab;
    public override void InstallBindings()
    {
        Container.Bind<SaveLoadSystem>().FromComponentInNewPrefab(saveLoadSystemPrefab).AsSingle().NonLazy();
        Container.Bind<LevelLoader>().FromComponentInNewPrefab(levelLoaderPrefab).AsSingle().NonLazy();
    }
}
