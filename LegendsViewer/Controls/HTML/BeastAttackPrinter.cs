﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    class BeastAttackPrinter : HTMLPrinter
    {
        BeastAttack Attack;
        World World;

        public BeastAttackPrinter(BeastAttack attack, World world)
        {
            Attack = attack;
            World = world;
        }

        public override string GetTitle()
        {
            string beast;
            if (Attack.Beast != null)
            {
                if (Attack.Beast.Name.IndexOf(" ") > 0)
                    beast = Attack.Beast.Name.Substring(0, Attack.Beast.Name.IndexOf(" "));
                else
                    beast = Attack.Beast.Name;
            }
            else
                beast = "Unknown";

            return "Rampage of " + beast;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            PrintStyle();

            string beast = "UNKNOWN BEAST";
            if (Attack.Beast != null)
                beast = Attack.Beast.ToLink();

            HTML.AppendLine("The " + Attack.GetOrdinal(Attack.Ordinal) + " Rampage of " + beast + " in " + Attack.Site.ToLink() + "</br>");

            List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, Attack.Site);
            HTML.AppendLine("<table border=\"0\" width=\"" + (maps[0].Width + maps[1].Width + 10) + "\">");
            HTML.AppendLine("<tr>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("</tr></table></br>");

            HTML.AppendLine("<b>Event Log</b></br>");
            foreach (WorldEvent printEvent in Attack.GetSubEvents())
                if (!BeastAttack.Filters.Contains(printEvent.Type))
                    HTML.AppendLine(printEvent.Print(true, Attack) + "<br/><br/>");
            return HTML.ToString();
        }
    }
}
