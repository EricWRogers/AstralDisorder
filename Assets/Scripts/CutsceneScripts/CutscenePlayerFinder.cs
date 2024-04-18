using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Linq;
using UnityEngine.Animations;
using UnityEngine.Timeline;

public class CutscenePlayerFinder : MonoBehaviour
{
    private PlayableDirector director;
    private OmnicatLabs.CharacterControllers.CharacterController player;
    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        player = OmnicatLabs.CharacterControllers.CharacterController.Instance;

        AnimationTrack animation = director.playableAsset.outputs.ToList()[3].sourceObject as AnimationTrack;
        director.SetGenericBinding(animation, player.GetComponent<Animator>());
    }
}
