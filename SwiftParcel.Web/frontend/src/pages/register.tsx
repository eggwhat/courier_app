import {
  Alert,
  Button,
  Checkbox,
  Label,
  Spinner,
  TextInput,
} from "flowbite-react";
import React from "react";
import { HiInformationCircle, HiCheckCircle } from "react-icons/hi";
import { Footer } from "../components/footer";
import { Header } from "../components/header";
import { Loader } from "../components/loader";
import { register } from "../utils/api";

export default function Register() {
  const [loading, setLoading] = React.useState(true);

  const [email, setEmail] = React.useState("");
  const [username, setUsername] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [confirmPassword, setConfirmPassword] = React.useState("");

  const termsCheckboxRef = React.useRef<HTMLInputElement>(null);

  const [error, setError] = React.useState("");
  const [success, setSuccess] = React.useState("");

  const [registerLoading, setRegisterLoading] = React.useState(false);

  const onSubmit = (e: any) => {
    e.preventDefault();
    if (registerLoading) return;
    setError("");
    setSuccess("");
    setRegisterLoading(true);

    if (password !== confirmPassword) {
      setError("Passwords do not match!");
      setRegisterLoading(false);
      return;
    }

    if (!termsCheckboxRef.current?.checked) {
      setError("Please accept the terms and conditions!");
      setRegisterLoading(false);
      return;
    }

    register(username, password, email)
      .then((res) => {
        setSuccess(
          res?.data?.message || "Registration successful! Please login."
        );
        setUsername("");
        setPassword("");
        setConfirmPassword("");
        setEmail("");
        termsCheckboxRef.current!.checked = false;
      })
      .catch((err) => {
        setError(err?.response?.data?.message || "Something went wrong!");
      })
      .finally(() => {
        setRegisterLoading(false);
      });
  };

  return (
    <>
      {loading ? <Loader /> : null}
      <div className="container mx-auto px-4">
        <Header loading={loading} setLoading={setLoading} />
        <h1 className="mb-2 text-3xl font-bold text-gray-900 dark:text-white">
          Create an account
        </h1>
        <p className="mb-5">
          Please register below. If you already have an account, please login
          using the button in the top right corner.
        </p>
        {error ? (
          <Alert color="failure" icon={HiInformationCircle} className="mb-3">
            <span>
              <span className="font-bold">Error!</span> {error}
            </span>
          </Alert>
        ) : null}
        {success ? (
          <Alert color="success" icon={HiCheckCircle} className="mb-3">
            <span>
              <span className="font-bold">Success!</span> {success}
            </span>
          </Alert>
        ) : null}
        <form className="flex flex-col gap-4 mb-5" onSubmit={onSubmit}>
          <div>
            <div className="mb-2 block">
              <Label htmlFor="email" value="Your email" />
            </div>
            <TextInput
              id="email"
              type="email"
              placeholder="email@example.com"
              required={true}
              shadow={true}
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>
          <div>
            <div className="mb-2 block">
              <Label htmlFor="username" value="Your username" />
            </div>
            <TextInput
              id="username"
              type="text"
              required={true}
              shadow={true}
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div>
            <div className="mb-2 block">
              <Label htmlFor="password" value="Your password" />
            </div>
            <TextInput
              id="password"
              type="password"
              required={true}
              shadow={true}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
          <div>
            <div className="mb-2 block">
              <Label htmlFor="repeat-password" value="Repeat password" />
            </div>
            <TextInput
              id="repeat-password"
              type="password"
              required={true}
              shadow={true}
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          </div>
          <div className="flex items-center gap-2">
            <Checkbox id="agree" ref={termsCheckboxRef} />
            <Label htmlFor="agree">
              I agree with the{" "}
              <span className="text-blue-600 hover:underline dark:text-blue-500">
                terms and conditions
              </span>
            </Label>
          </div>
          {registerLoading ? (
            <Button type="submit" disabled={true}>
              <div className="mr-3">
                <Spinner size="sm" light={true} />
              </div>
              Registering ...
            </Button>
          ) : (
            <Button type="submit">Register new account</Button>
          )}
        </form>
        <Footer />
      </div>
    </>
  );
}
