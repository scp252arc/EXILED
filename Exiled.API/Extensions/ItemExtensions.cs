// -----------------------------------------------------------------------
// <copyright file="ItemExtensions.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Exiled.API.Enums;
    using Exiled.API.Features.Items;
    using Exiled.API.Structs;

    using InventorySystem;
    using InventorySystem.Items;

    /// <summary>
    /// A set of extensions for <see cref="ItemType"/>.
    /// </summary>
    public static class ItemExtensions
    {
        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is an ammo.
        /// </summary>
        /// <param name="item">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is an ammo or not.</returns>
        public static bool IsAmmo(this ItemType item) => item == ItemType.Ammo9x19 || item == ItemType.Ammo12gauge || item == ItemType.Ammo44cal || item == ItemType.Ammo556x45 || item == ItemType.Ammo762x39;

        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a weapon.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <param name="checkMicro">Indicates whether the MicroHID item should be taken into account or not.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is a weapon or not.</returns>
        public static bool IsWeapon(this ItemType type, bool checkMicro = true) => type == ItemType.GunCrossvec ||
            type == ItemType.GunLogicer || type == ItemType.GunRevolver || type == ItemType.GunShotgun ||
            type == ItemType.GunAK || type == ItemType.GunCOM15 || type == ItemType.GunCOM18 ||
            type == ItemType.GunE11SR || type == ItemType.GunFSP9 || (checkMicro && type == ItemType.MicroHID);

        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is an SCP.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is an SCP or not.</returns>
        public static bool IsScp(this ItemType type) => type == ItemType.SCP018 || type == ItemType.SCP500 || type == ItemType.SCP268 || type == ItemType.SCP207;

        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a throwable item.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is a throwable item or not.</returns>
        public static bool IsThrowable(this ItemType type) => type == ItemType.SCP018 || type == ItemType.GrenadeHE || type == ItemType.GrenadeFlash;

        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a medical item.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is a medical item or not.</returns>
        public static bool IsMedical(this ItemType type) => type == ItemType.Painkillers || type == ItemType.Medkit || type == ItemType.SCP500 || type == ItemType.Adrenaline;

        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a utility item.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is an utilty item or not.</returns>
        public static bool IsUtility(this ItemType type) => /*type == ItemType.Disarmer ||*/ type == ItemType.Flashlight || type == ItemType.Radio;

        /// <summary>
        /// Check if a <see cref="ItemType"/> is an armor item.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is an armor or not.</returns>
        public static bool IsArmor(this ItemType type) => type == ItemType.ArmorCombat || type == ItemType.ArmorHeavy ||
                                                          type == ItemType.ArmorLight;

        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a keycard.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is a keycard or not.</returns>
        public static bool IsKeycard(this ItemType type) =>
            type == ItemType.KeycardChaosInsurgency || type == ItemType.KeycardContainmentEngineer || type == ItemType.KeycardFacilityManager ||
            type == ItemType.KeycardGuard || type == ItemType.KeycardJanitor || type == ItemType.KeycardNTFCommander ||
            type == ItemType.KeycardNTFLieutenant || type == ItemType.KeycardO5 || type == ItemType.KeycardScientist ||
            type == ItemType.KeycardResearchCoordinator || type == ItemType.KeycardNTFOfficer || type == ItemType.KeycardZoneManager;

        /// <summary>
        /// Gets the default ammo of a weapon.
        /// </summary>
        /// <param name="item">The <see cref="ItemType">item</see> that you want to get durability of.</param>
        /// <returns>Returns the item durability.</returns>
        public static byte GetMaxAmmo(this ItemType item)
        {
            if (!InventoryItemLoader.AvailableItems.TryGetValue(item, out ItemBase itemBase) || !(itemBase is InventorySystem.Items.Firearms.Firearm firearm))
                return 0;

            return firearm.AmmoManagerModule.MaxAmmo;
        }

        /// <summary>
        /// Converts a valid ammo <see cref="ItemType"/> into an <see cref="AmmoType"/>.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/> to convert.</param>
        /// <returns>The ammo type of the given item type.</returns>
        public static AmmoType GetAmmoType(this ItemType type)
        {
            switch (type)
            {
                case ItemType.Ammo9x19:
                    return AmmoType.Nato9;
                case ItemType.Ammo556x45:
                    return AmmoType.Nato556;
                case ItemType.Ammo762x39:
                    return AmmoType.Nato762;
                case ItemType.Ammo12gauge:
                    return AmmoType.Ammo12Gauge;
                case ItemType.Ammo44cal:
                    return AmmoType.Ammo44Cal;
                default:
                    return AmmoType.None;
            }
        }

        /// <summary>
        /// Converts an <see cref="AmmoType"/> into it's corresponding <see cref="ItemType"/>.
        /// </summary>
        /// <param name="type">The <see cref="AmmoType"/> to convert.</param>
        /// <returns>The Item type of the specified ammo.</returns>
        public static ItemType GetItemType(this AmmoType type)
        {
            switch (type)
            {
                case AmmoType.Nato556:
                    return ItemType.Ammo556x45;
                case AmmoType.Nato762:
                    return ItemType.Ammo762x39;
                case AmmoType.Nato9:
                    return ItemType.Ammo9x19;
                case AmmoType.Ammo12Gauge:
                    return ItemType.Ammo12gauge;
                case AmmoType.Ammo44Cal:
                    return ItemType.Ammo44cal;
                default:
                    return ItemType.None;
            }
        }

        /// <summary>
        /// Converts a <see cref="GrenadeType"/> into the corresponding <see cref="ItemType"/>.
        /// </summary>
        /// <param name="type"><inheritdoc cref="GrenadeType"/></param>
        /// <returns><inheritdoc cref="ItemType"/></returns>
        public static ItemType GetItemType(this GrenadeType type)
        {
            switch (type)
            {
                case GrenadeType.Flashbang:
                    return ItemType.GrenadeFlash;
                case GrenadeType.Scp018:
                    return ItemType.SCP018;
                case GrenadeType.FragGrenade:
                    return ItemType.GrenadeHE;
                default:
                    return ItemType.None;
            }
        }

        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> of <see cref="Item"/>s into the corresponding <see cref="List{T}"/> of <see cref="ItemType"/>s.
        /// </summary>
        /// <param name="items">The items to convert.</param>
        /// <returns>A new <see cref="List{T}"/> of <see cref="ItemType"/>s.</returns>
        public static List<ItemType> GetItemTypes(this IEnumerable<Item> items)
        {
            List<ItemType> itemTypes = new List<ItemType>();
            itemTypes.AddRange(items.Select(item => item.Type));
            return itemTypes;
        }

        /// <summary>
        /// Gets all <see cref="AttachmentIdentifier"/>s present on an <see cref="ItemType"/>.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/> to iterate over.</param>
        /// <param name="code">The <see cref="uint"/> value which represents the attachments code to check.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="AttachmentIdentifier"/> value which represents all the attachments present on the specified <see cref="ItemType"/>.</returns>
        public static IEnumerable<AttachmentIdentifier> GetAttachments(this ItemType type, uint code)
        {
            if ((uint)type.GetBaseCode() > code)
            {
                throw new System.ArgumentException("The attachments code can't be less than the item's base code.");
            }

            code -= (uint)type.GetBaseCode();
            return GetCombinations(Firearm.AvailableAttachments[type].Select(identifier =>
            identifier.Code).ToArray()).Where(items => items.Sum() == code).FirstOrDefault().Select(target =>
            Firearm.AvailableAttachments[type].FirstOrDefault(attId => attId.Code == target));
        }

        /// <summary>
        /// Tries to get all <see cref="AttachmentIdentifier"/>s present on an <see cref="ItemType"/>.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/> to iterate over.</param>
        /// <param name="code">The <see cref="uint"/> value which represents the attachments code to check.</param>
        /// <param name="identifiers">The attachments present on the specified <see cref="ItemType"/>.</param>
        /// <returns><see langword="true"/> if the specified <see cref="ItemType"/> is a weapon.</returns>
        public static bool TryGetAttachments(this ItemType type, uint code, out IEnumerable<AttachmentIdentifier> identifiers)
        {
            identifiers = default;

            if (!type.IsWeapon())
                return false;

            identifiers = GetAttachments(type, code);

            return true;
        }

        /// <summary>
        /// Gets the value resulting from the sum of all elements within a specific <see cref="IEnumerable{T}"/> of <see cref="AttachmentIdentifier"/>.
        /// </summary>
        /// <param name="identifiers">The <see cref="IEnumerable{T}"/> of <see cref="AttachmentIdentifier"/> to compute.</param>
        /// <returns>A <see cref="uint"/> value that represents the attachments code.</returns>
        public static uint GetAttachmentsCode(this IEnumerable<AttachmentIdentifier> identifiers)
        {
            uint code = 0;

            foreach (AttachmentIdentifier identifier in identifiers)
                code += identifier;

            return code;
        }

        /// <summary>
        /// Gets the <see cref="BaseCode"/> of the specified <see cref="ItemType"/>.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/> to check.</param>
        /// <returns>The corresponding <see cref="BaseCode"/>.</returns>
        public static BaseCode GetBaseCode(this ItemType type)
        {
            if (!type.IsWeapon())
                return 0;

            return Firearm.FirearmPairs[type];
        }

        // This is an extension for IEnumerable to sum uint values.
        // Credit: "TheGeneral" on StackOverflow
        private static uint Sum(this IEnumerable<uint> source)
        {
            uint sum = 0;
            checked
            {
                return source.Aggregate(sum, (current, v) => current + v);
            }
        }

        // This determines what attachment codes can be added together
        // to give us the combined code we have, so that we can determine
        // which attachments are present for any given value.
        // Credits: "TheGeneral" on StackOverflow
        // https://stackoverflow.com/questions/69762657/how-to-find-a-given-number-by-adding-up-numbers-from-list-of-numbers-and-return
        private static IEnumerable<T[]> GetCombinations<T>(T[] source)
        {
            for (int i = 0; i < (1 << source.Length); i++)
                yield return source.Where((t, j) => (i & (1 << j)) != 0).ToArray();
        }
    }
}