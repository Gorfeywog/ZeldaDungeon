﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon
{
	public class BlueUnwalkableGapBlock : IBlock
	{
		public class BlueUnwalkableGapBlock : IBlock
		{
			private ISprite sprite = BlockSpriteFactory.Instance.CreateBlueUnwalkableGapBlock(); // TODO: Check with Luke that this is correct.
			private static int width = 16;
			private static int height = 16;
			public Point CurrentPoint { get; set; }
			public BlueUnwalkableGapBlock(Point position)
			{
				CurrentPoint = position;
			}
			public void Draw(SpriteBatch spriteBatch)
			{
				sprite.Draw(spriteBatch, CurrentPoint);
			}
			public void UpdateSprite() => sprite.Update();
		}
	}
}

