using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MergeManager mergeManager;
    [SerializeField] private VfxManager vfxManager;
    [SerializeField] private SoundManager soundManager;
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(gameManager);
        Container.Bind<MergeManager>().FromInstance(mergeManager);
        Container.Bind<VfxManager>().FromInstance(vfxManager);
        Container.Bind<SoundManager>().FromInstance(soundManager);
    }
}