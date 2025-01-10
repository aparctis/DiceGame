using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SaveLoadSystem saveLoadSystemPrefab; 
    [SerializeField] private LevelLoader levelLoaderPrefab;
    [SerializeField] private GameSet defaultGameSet;

    public override void InstallBindings()
    {
        Container.BindInstance(defaultGameSet).AsSingle();

        Container.Bind<SaveLoadSystem>()
                .FromComponentInNewPrefab(saveLoadSystemPrefab)
                .AsSingle()
                .NonLazy();

        Container.Bind<LevelLoader>()
                .FromComponentInNewPrefab(levelLoaderPrefab)
                .AsSingle()
                .NonLazy();
    }
}