package service;

import filter.IFilter;

public interface FilterService {
    IFilter get(String name);
    void add(String name, IFilter filter);
    boolean exists(String name);
}
