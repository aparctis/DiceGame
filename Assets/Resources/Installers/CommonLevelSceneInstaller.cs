using UnityEngine;
using Zenject;

public class CommonLevelSceneInstaller : MonoInstaller
{

    [SerializeField] private ActionObjectPool _actionObjectPool;
    [SerializeField] private ActionDecorPool _decorePool;
    [SerializeField] private PositionConverter _positionConverterPrefab;
    [SerializeField] private SwipeDetector _swipeDetectorPrefab;


    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ActionObjectPool>().FromComponentInNewPrefab(_actionObjectPool).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ActionDecorPool>().FromComponentInNewPrefab(_decorePool).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PositionConverter>().FromComponentInNewPrefab(_positionConverterPrefab).AsSingle();
        Container.BindInterfacesAndSelfTo<SwipeDetector>().FromComponentInNewPrefab(_swipeDetectorPrefab).AsSingle().NonLazy();
    }

}
