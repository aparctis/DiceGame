using UnityEngine;

internal class PlayerAvatarManager:MonoBehaviour
{
    [SerializeField] private AvatarModel avatarModel;
    public AvatarModel getPlayerAvatar()
    {
        avatarModel?.Alive();
        return avatarModel;
    }

}