package collection;

import java.util.*;

public class ArrayList<T> extends AbstractList<T> {

	private final int INITIAL_CAPACITY = 10;
	private final int LOAD_FACTOR = 2;
	private Object[] data;
	private int size = 0;
	private int capacity = INITIAL_CAPACITY;

	public ArrayList() {
		data = new Object[INITIAL_CAPACITY];
	}

	private T data(int index) {
		return (T) data[index];
	}

	@Override
	public int size() {
		return size;
	}

	@Override
	public boolean isEmpty() {
		return size == 0;
	}

	@Override
	public boolean contains(Object o) {
		return indexOf(o) != -1;
	}

	@Override
	public Object[] toArray() {
		return Arrays.copyOf(data, size);
	}

	@Override
	public <T1> T1[] toArray(T1[] a) {
		if (a.length < size)
			return (T1[]) Arrays.copyOf(data, size, a.getClass());
		System.arraycopy(data, 0, a, 0, size);
		if (a.length > size)
			a[size] = null;
		return a;
	}

	@Override
	public void add(int index, T element) {
		checkIndexInBoundsWithSize(index);
		shiftRight(index, 1);
		data[index] = element;
	}

	@Override
	public boolean addAll(Collection<? extends T> c) {
		return addAll(size, c);
	}

	@Override
	public boolean addAll(int index, Collection<? extends T> c) {
		if (c.isEmpty())
			return false;
		shiftRight(index, c.size());
		for (var element : c)
			data[index++] = element;
		return true;
	}

	@Override
	public T remove(int index) {
		checkIndexInBoundsWithSize(index);
		var value = data(index);
		shiftRight(index + 1, -1);
		return value;
	}

	@Override
	public boolean remove(Object o) {
		var index = indexOf(o);
		if (index != -1)
			remove(indexOf(o));
		return index != -1;
	}

	@Override
	public boolean removeAll(Collection<?> c) {
		var result = false;
		for (var elem : c)
			result = remove(elem) || result;
		return result;
	}

	@Override
	public boolean containsAll(Collection<?> c) {
		var result = true;
		for (var elem : c)
			result = contains(elem) && result;
		return result;
	}

	@Override
	public boolean retainAll(Collection<?> c) {
		var foundCounter = 0;
		for (int i = 0; i < size; i++) {
			if (c.contains(data[i])) {
				data[foundCounter] = data(i);
				foundCounter++;
			}
		}
		var oldSize = size;
		updateSize(foundCounter);
		return size == oldSize;
	}

	@Override
	public T get(int index) {
		checkIndexInBounds(index);
		return data(index);
	}

	@Override
	public T set(int index, T element) {
		checkIndexInBounds(index);
		var oldValue = get(index);
		data[index] = element;
		return oldValue;
	}

	@Override
	public List<T> subList(int fromIndex, int toIndex) {
		checkIndexInBoundsWithSize(fromIndex);
		checkIndexInBoundsWithSize(toIndex);
		if (fromIndex > toIndex)
			throw new IndexOutOfBoundsException();

		var result = new ArrayList<T>();
		for (int i = fromIndex; i < toIndex; i++)
			result.add(data(i));
		return result;
	}

	private void shiftRight(int index, int shiftSize) {
		var copyRange = Arrays.copyOfRange(data, index, size);
		updateSize(size + shiftSize);
		for (int i = 0; i < copyRange.length; i++) {
			data[index + shiftSize + i] = copyRange[i];
		}
	}

	private void updateSize(int newSize) {
		while (newSize > capacity)
			grow();
		size = newSize;
	}

	private void grow() {
		capacity *= LOAD_FACTOR;
		data = Arrays.copyOf(data, capacity);
	}

	private void checkIndexInBoundsWithSize(int index) {
		if (index < 0 || index > size)
			throw new IndexOutOfBoundsException(outOfBoundsMessage(index));
	}

	private void checkIndexInBounds(int index) {
		if (index < 0 || index >= size)
			throw new IndexOutOfBoundsException(outOfBoundsMessage(index));
	}

	private String outOfBoundsMessage(int index) {
		return "Index: " + index + ", Size: " + size;
	}

}
