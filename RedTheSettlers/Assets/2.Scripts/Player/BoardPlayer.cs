using RedTheSettlers.Tiles;
using RedTheSettlers.GameSystem;
using RedTheSettlers.UnitTest;
using RedTheSettlers.Skills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.Users
{
    public class BoardPlayer : User
    {
        private int weaponLevel;
        private int shieldLevel;
        public List<Skill> SkillList = new List<Skill>(3);
        public TileOwner tileOwner;

        public void InitializePlayerData(int weaponLevel, int shieldLevel, List<Skill> skillList)
        {
            this.weaponLevel = weaponLevel;
            this.shieldLevel = shieldLevel;
            SkillList = skillList;
        }

        public void SetSkillSlot(SkillType skillType, int slotNum)
        {
            switch(skillType)
            {
                case SkillType.Melee:
                    SkillList[slotNum] = new MeleeAttackSkill();
                    break;
                case SkillType.Range:
                    SkillList[slotNum] = new RangeAttackSkill();
                    break;
                case SkillType.SpeedUpBuff:
                    SkillList[slotNum] = new SpeedUpBuffSkill();
                    break;
                case SkillType.OverWhelmBuff:
                    SkillList[slotNum] = new OverWhelmBuffSkill();
                    break;
            }
        }

        public void MoveToTargetTile(BoardTile targetTile)
        {
            PossessingTile.Add(targetTile);

            inventory[(int)(targetTile.TileType)].Count++;

            transform.position = new Vector3(targetTile.transform.position.x, transform.position.y, targetTile.transform.position.z);

            int[] coordX = { 1, 0, -1, -1, 0, 1 };
            int[] coordZ = { 0, 1, 1, 0, -1, -1 };

            for (int i = 0; i < 6; i++)
            {
                BoardTile targetBoardTile;
                if (TileManager.Instance.BoardTileGrid[targetTile.TileCoordinate.x + coordX[i], targetTile.TileCoordinate.z + coordZ[i]] != null)
                {
                    targetBoardTile = TileManager.Instance.BoardTileGrid[targetTile.TileCoordinate.x + coordX[i], targetTile.TileCoordinate.z + coordZ[i]].GetComponent<BoardTile>();
                }
                else
                {
                    continue;
                }

                if (targetBoardTile.tileOwner == tileOwner)
                {
                    targetTile.TileBorder[i].SetActive(false);
                    targetBoardTile.TileBorder[(i + 3) % 6].SetActive(false);
                }
                else
                {
                    targetTile.TileBorder[i].SetActive(true);
                }
            }
        }

        public void PossessTile(BoardTile targetTile)
        {
            PossessingTile.Add(targetTile);
        }

        public override void ChangeItemCount(ItemData[] itemList)
        {
            for (int i = 0; i < GlobalVariables.MaxItemNumber; i++)
            {
                inventory[i].Count += itemList[i].Count;
            }
        }
    }
}
