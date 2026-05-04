import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form } from 'react-bootstrap';
import type { Customer, CustomerType } from '../api';
import CustomerForm from './CustomerForm';
import CustomerTypeSelect from './CustomerTypeSelect';

export default function CustomerPage() {
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [types, setTypes] = useState<CustomerType[]>([]);
    const [showModal, setShowModal] = useState(false);
    const [editing, setEditing] = useState<Customer | null>(null);

    // Load data
    const load = async () => {
        const custRes = await fetch('/api/customers');
        setCustomers(await custRes.json());

        const typeRes = await fetch('/api/customertypes');
        setTypes(await typeRes.json());
    };

    useEffect(() => { load(); }, []);

    const handleDelete = async (id: number) => {
        if (!window.confirm('Are you sure?')) return;
        await fetch(`/api/customers/${id}`, { method: 'DELETE' });
        load();
    };

    const handleSave = async (form: Customer) => {
        const payload = {
            id: form.id,
            name: form.name,
            description: form.description,
            address: form.address,
            city: form.city,
            state: form.state,
            zip: form.zip,
            lastUpdated: form.lastUpdated ?? new Date().toISOString(),
            customerTypeId: form.customerTypeId
        };
        debugger
        if (form.id) {
            debugger
            await fetch(`/api/customers/${form.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });
        } else {
            await fetch('/api/customers', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });
        }
        setShowModal(false);
        setEditing(null);
        load();
    };

    return (
        <div className="container mt-4">
            <h2>Customers</h2>
            <Button className="mb-3" onClick={() => { setEditing(null); setShowModal(true); }}>
                Add Customer
            </Button>

            <Table striped bordered hover>
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
                                <Button size="sm" variant="warning" onClick={() => { setEditing(c); setShowModal(true); }}>Edit</Button>{' '}
                                <Button size="sm" variant="danger" onClick={() => handleDelete(c.id)}>Delete</Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <Modal show={showModal} onHide={() => { setShowModal(false); setEditing(null); }}>
                <Modal.Header closeButton>
                    <Modal.Title>{editing ? 'Edit Customer' : 'Add Customer'}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <CustomerForm
                        types={types}
                        initial={editing}
                        onSaved={handleSave}
                        onCancel={() => { setShowModal(false); setEditing(null); }}
                    />
                </Modal.Body>
            </Modal>
        </div>
    );
}