import React, { useEffect, useState } from 'react';
import type { Customer, CustomerType } from '../api';
import CustomerForm from './CustomerForm';

export default function CustomerList() {
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [types, setTypes] = useState<CustomerType[]>([]);
    const [editing, setEditing] = useState<Customer | null>(null);
    const [showForm, setShowForm] = useState(false);

    const load = async () => {
        
        const ct = await fetch('/api/customertypes').then(r => r.json());
        const c = await fetch('/api/customers').then(r => r.json());
        setTypes(ct);
        setCustomers(c);
    };

    useEffect(() => { load(); }, []);

    const onDelete = async (id?: number) => {
        if (!id) return;
        if (!confirm('Delete customer?')) return;
        await fetch(`/api/customers/${id}`, { method: 'DELETE' });
        await load();
    };

    return (
        <div>
            <h2>Customers</h2>

            <button
                className="btn btn-primary mb-3"
                onClick={() => { setEditing(null); setShowForm(true); }}
            >
                Add Customer
            </button>

            {showForm && (
                <CustomerForm
                    types={types}
                    initial={editing}
                    onSaved={async () => {
                        setShowForm(false);
                        await load();
                    }}
                    onCancel={() => setShowForm(false)}
                />
            )}

            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Type</th>
                        <th>Address</th>
                        <th>City</th>
                        <th>State</th>
                        <th>Zip</th>
                        <th>Last Updated</th>
                        <th>Actions</th>
                    </tr>
                </thead>

                <tbody>
                    {customers.map(c => (
                        <tr key={c.id}>
                            <td>{c.name}</td>
                            <td>{c.customerType?.name ?? c.customerTypeId}</td>
                            <td>{c.address}</td>
                            <td>{c.city}</td>
                            <td>{c.state}</td>
                            <td>{c.zip}</td>
                            <td>{c.lastUpdated ? new Date(c.lastUpdated).toLocaleString() : ''}</td>
                            <td>
                                <button
                                    className="btn btn-sm btn-warning me-2"
                                    onClick={() => { setEditing(c); setShowForm(true); }}
                                >
                                    Edit
                                </button>

                                <button
                                    className="btn btn-sm btn-danger"
                                    onClick={() => onDelete(c.id)}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
