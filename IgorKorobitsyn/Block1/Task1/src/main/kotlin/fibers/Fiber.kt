package fibers

public class Fiber(
    private var action: () -> Unit
) {

    private var id: Int = 0

    fun getId(): Int{
        return id
    }

    init {
        create()
    }

    private var primaryId: Int = 0

    fun getPrimaryId(): Int{
        return primaryId
    }

    private var isPrimary: Boolean = false

    fun isPrimary(): Boolean{
        return isPrimary
    }

    fun delete(){
        //unmanaged
    }

    fun delete(id: Int){
        //unmanaged
    }

    fun switch(id: Int){
        //unmanaged
    }

    private fun create(){
        if (primaryId == 0){
            primaryId = UnmanagedFiberAPI.count // = UnmanagedFiberAPI.kernel32.ConvertThreadToFiber(0)
            isPrimary = true;
        }
        val lpFiber = EventCallbackInterface { param -> fiberRunnerProc(param) }


        id = UnmanagedFiberAPI.count++ // = UnmanagedFiberAPI.kernel32.CreateFiber(10050, lpFiber, 0)
    }

    private fun fiberRunnerProc(lpParam: Int): Int {
        var status = 0

        try {
            action()
        } catch (e: Exception) {
            status = 1
            throw e
        } finally {
            if (status == 1) {
                // UnmanagedFiberAPI.kernel32.DeleteFiber(id)
            }
        }

        return 0
    }
}