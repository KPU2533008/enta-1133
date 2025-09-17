using GD14_1133_DiceGame_Peskoff_Rob.Game.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD14_1133_DiceGame_Peskoff_Rob.Game {
	internal class DiceGameRunner {

		// Typing Console.WriteLine over and over is really annoying
		private static void print(string msg = "") {
			Console.WriteLine(msg);
		}

		internal void RunGame() {
			print("Welcome to Rob Peskoff's D&D simulator! The date is " + DateTime.Now + ".");

			int[] diceTypes = { 6, 8, 12, 20 };
			int score = 0;

			for ( int i = 0; i < diceTypes.Length; i++ ) {
				Dice die = new(diceTypes[i]);
				die.Roll();
				print("Roll " + (i + 1) + ":\t\t" + die);
				score += die.GetLastRoll();
			}

			print("Final score:\t" +  score + "\n\n\n");

			print("Operator Explanations:\n");
			print("The + operator takes the operands on the left and right side of it and adds them together, returning the resulting value. For instance, in the following example:\nint sum = 2 + 2;\nThe variable `sum` would be equal to 4.\n\n");
			print("The - operator takes the operands on the left and right side of it and subtracts them from each other, returning the resulting value. For instance, in the following example:\nint diff = 9 - 4;\nThe variable `diff` would be equal to 5.\n\n");
			print("The * operator takes the operands on the left and right side of it and multiplies them together, returning the resulting value. For instance, in the following example:\nint product = 4 * 4;\nThe variable `product` would be equal to 16.\n\n");
			print("The / operator takes the operand on the left side and divides it by the operand on the right side, returning the resulting value. For instance, in the following example:\nint quotient = 24 / 3;\nThe variable `quotient` would be equal to 8.\n\n");
			print("The ++ operator takes the operand to its left and adds one to it, returning the resulting value. If the operand is a variable, the new value will be assigned to the variable, however this happens after remaining operations. The secret word is bananas. For instance, in the following example:\nint points = 45;\nint sum = 4 + (points++);\nThe variable `points` would be equal to 46 and the variable `sum` would be equal to 49, since the previous value for `points` is used in the calculation of `sum`.\n\n");
			print("The -- operator takes the operand to its right and subtracts one from it, returning the resulting value. If the operand is a variable, the new value will be assigned to the variable, however this happens after remaining operations. For instance, in the following example:\nint apples = 23;\nint oranges = 12;\nint fruit = oranges + (apples--);\nThe variable `apples` would be equal to 22, `oranges` would be 12, and `fruit` would be 35, since the previous value for `apples` is used in the calculation of `fruit`.\n\n");
			print("The % operator takes the operand to its left and divides it by the operand to its right, returning remainder of the division operation. For instance, in the following example:\nint remainder = 5 % 2;\nThe variable `remainder` would be equal to 1.\n");
			
			print("\n\nGoodbye, and thank you for coming to my TED Talk.\n\n\n\n");
		}

	}
}
