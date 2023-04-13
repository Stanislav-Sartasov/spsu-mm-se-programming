package fibers

import java.lang.Exception

class Fiber(private var action: () -> Unit){

    private var id: Int = 0

    fun getId(): Int {
        return id
    }

    private var primaryId: Int = 0

    private var isPrimary: Boolean = false

    fun isPrimary(): Boolean{
        if (isPrimary && id == primaryId){
            isPrimary = true
            return true
        }
        else{
            isPrimary = false
            return false
        }
    }

    init {
        create()
    }

    private fun create(){
        if (primaryId == 0){
            primaryId = UnmanagedFiberAPI.count // = UnmanagedFiberAPI.kernel32.ConvertThreadToFiber(0)
            isPrimary = true;
        }
        val lpFiber = EventCallbackInterface { param -> fiberRunnerProc(param) }


        id = UnmanagedFiberAPI.count++ // = UnmanagedFiberAPI.kernel32.CreateFiber(10050, lpFiber, 0)
    }

    private fun fiberRunnerProc(param: Int): Int{
        var status: Int = 0
        try{
            action()
        } catch (e: Exception) {
            status = 1
            throw e
        } finally {
            if (status == 1){
                //UnmanagedFiberAPI.kernel32.DeleteFiber(id)
            }
        }
        return status;
    }

    fun delete(){
        delete(id)
    }


    fun delete(fiberId: Int){
        //UnmanagedFiberAPI.kernel32.DeleteFiber(fiberId)
    }

    fun switch(fiberId: Int){
        //UnmanagedFiberAPI.kernel32.SwitchToFiber(fiberId)
    }
}