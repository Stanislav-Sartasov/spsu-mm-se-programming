package bmp.bmp.filters

import bmp.bmp.AKernelFilter

class MedianFilter(override var radius: Int) : AKernelFilter() {
    private var diameterSquared = (2 * radius + 1) * (2 * radius + 1)
    override var kernel = List(diameterSquared) { 1f / diameterSquared }
}