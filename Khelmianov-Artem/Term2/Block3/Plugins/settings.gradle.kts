
rootProject.name = "Plugins"
include("game")
include("bots:MartingaleBot")
findProject(":bots:MartingaleBot")?.name = "MartingaleBot"
include("bots:RndNumberBot")
findProject(":bots:RndNumberBot")?.name = "RndNumberBot"
include("bots:RndDozenBot")
findProject(":bots:RndDozenBot")?.name = "RndDozenBot"
