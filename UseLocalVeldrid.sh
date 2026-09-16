#!/bin/sh

# Run this script to use a local copy of veldrid rather than fetching it from nuget.
# It expects the veldrid directory to be at the same level as the osu-framework directory
#
# https://github.com/ppy/osu-framework/wiki/Testing-local-framework-checkout-with-other-projects

FRAMEWORK_CSPROJ="osu.Framework/osu.Framework.csproj"
SLN="osu-framework.sln"

dotnet remove $FRAMEWORK_CSPROJ reference ppy.Veldrid ppy.Veldrid.SPIRV

dotnet sln $SLN add ../neo-veldrid/src/NeoVeldrid/NeoVeldrid.csproj ../neo-veldrid/src/NeoVeldrid.SPIRV/NeoVeldrid.SPIRV.csproj

dotnet add $FRAMEWORK_CSPROJ reference ../neo-veldrid/src/NeoVeldrid/NeoVeldrid.csproj ../neo-veldrid/src/NeoVeldrid.SPIRV/NeoVeldrid.SPIRV.csproj

tmp=$(mktemp)

jq '.solution.projects += ["../neo-veldrid/src/NeoVeldrid/NeoVeldrid.csproj", "../neo-veldrid/src/NeoVeldrid.SPIRV/NeoVeldrid.SPIRV.csproj"]' osu-framework.Desktop.slnf > $tmp
mv -f $tmp osu-framework.Desktop.slnf

jq '.solution.projects += ["../neo-veldrid/src/NeoVeldrid/NeoVeldrid.csproj", "../neo-veldrid/src/NeoVeldrid.SPIRV/NeoVeldrid.SPIRV.csproj"]' osu-framework.Android.slnf > $tmp
mv -f $tmp osu-framework.Android.slnf

jq '.solution.projects += ["../neo-veldrid/src/NeoVeldrid/NeoVeldrid.csproj", "../neo-veldrid/src/NeoVeldrid.SPIRV/NeoVeldrid.SPIRV.csproj"]' osu-framework.iOS.slnf > $tmp
mv -f $tmp osu-framework.iOS.slnf
