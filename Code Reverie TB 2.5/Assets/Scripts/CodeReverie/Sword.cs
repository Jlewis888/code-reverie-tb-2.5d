using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class Sword : WeaponObject, IWeapon
    {

        public Animator animator;
        public PlayerMovementController playerMovementController;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerMovementController = GetComponentInParent<PlayerMovementController>();
        }

        public override void Attack(Transform firePoint)
        {
            animator.SetTrigger("Attack");
        }
    }
}
