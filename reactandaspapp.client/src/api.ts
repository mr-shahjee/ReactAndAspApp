const BASE = import.meta.env.VITE_API_BASE || 'https://localhost:7085/api';

export type CustomerType = { id: number; name: string; };
export type Customer = {
    id?: number;
    name: string;
    customerTypeId: number;
    description?: string | null;
    address: string;
    city: string;
    state: string;
    zip: string;
    lastUpdated?: string;
    customerType?: CustomerType; // included from API
};

async function request<T>(path: string, opts: RequestInit = {}): Promise<T> {
    const res = await fetch(`${BASE}${path}`, {
        headers: { 'Content-Type': 'application/json' },
        ...opts
    });
    if (!res.ok) {
        const txt = await res.text();
        throw new Error(txt || res.statusText);
    }
    return (await res.json()) as T;
}

export const api = {
    getCustomerTypes: () => request<CustomerType[]>('/customertypes'),
    getCustomers: () => request<Customer[]>('/customers'),
    getCustomer: (id: number) => request<Customer>(`/customers/${id}`),
    createCustomer: (c: Customer) => request<Customer>('/customers', { method: 'POST', body: JSON.stringify(c) }),
    updateCustomer: (id: number, c: Customer) => fetch(`${BASE}/customers/${id}`, { method: 'PUT', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(c) }),
    deleteCustomer: (id: number) => fetch(`${BASE}/customers/${id}`, { method: 'DELETE' })
};
