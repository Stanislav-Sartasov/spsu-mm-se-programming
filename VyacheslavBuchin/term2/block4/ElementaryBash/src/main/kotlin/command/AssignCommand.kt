package command

import channel.StringChannel
import exception.ElementaryBashException
import service.substitution.SubstitutionManager

class AssignCommand(private val substitutionManager: SubstitutionManager) : Command {
	override var output = StringChannel.nullChannel()
	override var error = StringChannel.nullChannel()
	override var input = StringChannel.nullChannel()

	override fun execute(args: Array<String>): Int {
		if (args.size > 1)
			throw ElementaryBashException(ElementaryBashException.INVALID_ARGUMENTS, "assignment command must have no arguments")
		val substitution = args[0]
		val name = substitution.substringBefore("=")
		val value = substitution.substringAfter("=")
		substitutionManager[name] = value
		return 0
	}

}