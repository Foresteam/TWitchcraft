using Terraria;

namespace TWitchery.Recipes;
/// <summary>No actual difference. Just fits best</summary>
class AltarRecipe : WitcheryRecipe {
	public AltarRecipe(float energyCost, float failedWorkedChance = 0, float matchThreshold = .75f) : base(energyCost, failedWorkedChance, matchThreshold) {}
	public AltarRecipe SetTarget(Item target) => (AltarRecipe)SetCatalyst(target);
}