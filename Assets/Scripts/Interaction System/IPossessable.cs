using UnityEngine;

namespace Spirit.Interaction
{
    public interface IPossessable
    {

        Sprite PossessSprite();
        void PossessStart();
        void PossessEnd();

    }
}
