using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatarManager : MonoBehaviour
{
    [SerializeField] private AvatarSet[] avatars;

    public void ActivateAvatar(int index)
    {
        foreach(AvatarSet avatar in avatars) avatar.Hide();

        avatars[index]?.Show();

    }

    [System.Serializable]
    public class AvatarSet
    {
        public GameObject avatar;
        public GameObject camera;

        public void Hide()
        {
            avatar.SetActive(false);
            camera.SetActive(false);
        }

        public void Show()
        {
            avatar.SetActive(true);
            camera.SetActive(true);
        }
    }
}

