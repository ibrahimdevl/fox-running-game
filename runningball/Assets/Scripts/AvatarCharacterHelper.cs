using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script helps replace the ball with an avatar/robot model.
/// 
/// SETUP INSTRUCTIONS:
/// 1. Remove the MeshFilter and MeshRenderer components from the Player GameObject
/// 2. Drag your avatar/robot model as a child of the Player GameObject
/// 3. Scale and position the avatar/robot so it fits around the SphereCollider (radius ~0.5)
/// 4. Attach this script to the Player GameObject or the avatar model
/// 5. The physics and collision will work as before using the SphereCollider
/// 
/// RECOMMENDED AVATAR SOURCES:
/// - Unity Asset Store: Search "free avatar" or "free robot"
/// - Sketchfab: https://sketchfab.com (many free models)
/// - Mixamo: https://www.mixamo.com (free animations + models)
/// 
/// POPULAR FREE MODELS:
/// - Simple Robot: Look for "free robot model" on Asset Store
/// - Humanoid Avatar: "Mannequin" or similar humanoid models
/// - Low-poly Characters: Search for "low poly character"
/// </summary>
/// 
public class AvatarCharacterHelper : MonoBehaviour
{
    [SerializeField]
    private Transform avatarModel;

    [SerializeField]
    private bool useAnimations = true;

    [SerializeField]
    private float rotationSpeed = 300.0f;

    private Animator animator;
    private Movement movement;
    private bool isRunning = false;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        
        if (avatarModel != null)
        {
            animator = avatarModel.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        // Check if game is running and trigger running animation
        if (useAnimations && animator != null)
        {
            bool shouldRun = movement != null && movement.gameObject.activeSelf;
            
            if (shouldRun != isRunning)
            {
                isRunning = shouldRun;
                animator.SetBool("IsRunning", isRunning);
            }
        }
    }

    /// <summary>
    /// Call this if you want to change the avatar model at runtime
    /// </summary>
    public void SetAvatarModel(Transform newAvatar)
    {
        if (avatarModel != null)
        {
            Destroy(avatarModel.gameObject);
        }
        avatarModel = newAvatar;
        
        if (avatarModel != null)
        {
            animator = avatarModel.GetComponent<Animator>();
        }
    }
}
