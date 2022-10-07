using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace TWitchery;
partial class WitcheryRecipe {
	#nullable enable
	public class Result
	{
		public struct ItemResult
		{
			public Item self;
			public Vector2 pos;
			public ItemResult(Item self, Vector2 pos = new Vector2())
			{
				this.self = self;
				this.pos = pos;
			}
		}
		public struct LiquidResult
		{
			public Liquid self;
			public LiquidResult(Liquid self)
			{
				this.self = self;
			}
		}
		public List<ItemResult> items;
		public List<LiquidResult> liquids;
		public float energyCost;
		public Result(float energyCost)
		{
			items = new List<ItemResult>();
			liquids = new List<LiquidResult>();
			this.energyCost = energyCost;
		}
		public Result Clone()
		{
			return (Result)this.MemberwiseClone();
		}
	}
}