﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using RTFP.DataStructures;
using RTFP.DataStructures.Geometry;
using RTFP.Generator.FloorPlan;

namespace RTFP
{
	static class Program
	{
		static void Main()
		{
			SuburbanGenerator generator = new SuburbanGenerator();

			while (true)
			{
				FloorPlan fp = generator.GenerateFloorPlan();
				DrawFloorPlan(fp);
			}
		}

		private static void DrawFloorPlan(FloorPlan fp)
		{
			int width = 252, height = 252;
			
			// Create our canvas to work with
			var bmp = new Bitmap(width, height);
			var gfx = Graphics.FromImage(bmp);
			gfx.FillRectangle(Brushes.White, new RectangleF(0, 0, width, height));

			// Draw vertices and edges
			foreach (Edge e in fp.Edges)
			{
				gfx.DrawLine(Pens.Black,
					new Point(e.Source.X, e.Source.Y),
					new Point(e.Destination.X, e.Destination.Y));

				gfx.DrawRectangle(Pens.Red, new Rectangle(new Point(e.Source.X - 1, e.Source.Y - 1), new Size(2, 2)));
				gfx.DrawRectangle(Pens.Red, new Rectangle(new Point(e.Destination.X - 1, e.Destination.Y - 1), new Size(2, 2)));
			}

			// Display form
			var form = new Form() { AutoSize = true };
			form.Controls.Add(new PictureBox() { Width = width, Height = height, Image = bmp, Location = new Point(5, 5) });
			form.ShowDialog();
		}
	}
}
