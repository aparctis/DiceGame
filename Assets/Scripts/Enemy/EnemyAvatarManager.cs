using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatarManager : MonoBehaviour
{
    [SerializeField] private AvatarSet[] avatars;

    public AvatarModel getEnemyAvatar(int index)
    {
        foreach(AvatarSet avatar in avatars) avatar.Hide();
        if(avatars.Length<=index)
        {
            Debug.LogError("Wrong enemy avatar index");
            return null;
        }
        else
        {

            avatars[index].Show();
            return avatars[index].model;
        }

    }

    [System.Serializable]
    public class AvatarSet
    {
        public GameObject avatar;
        public GameObject camera;
        public AvatarModel model;

        public void Hide()
        {
            avatar.SetActive(false);
            camera.SetActive(false);
        }

        public void Show()
        {
            model.Alive();
            avatar.SetActive(true);
            camera.SetActive(true);
        }
    }
}

